export default class AllQuizzesCtrl {
    /** @ngInject */
    constructor(api, route, $stateParams) {
        this._route = route;
        this.api = api;

        const COUNT_OF_QUIZZES = $stateParams.count;
        this.quizzes = [];
        this.categories = [];
        this.selectedCategory = "";

        this.api.getPreviews(COUNT_OF_QUIZZES).then(res => {
            this.quizzes = res.data.quizzes;
            this.categories = res.data.categories;
        }).catch(res => {
            this._route.error(res.data);
        });
    }

    openQuiz(id) {
        this._route.go('quiz', id, { id: id });
    }

    filterCategory() {
        this.api.getPreviews(0, 0, this.selectedCategory).then(res => {
            this.quizzes = res.data.quizzes;
            this.categories = res.data.categories;
        }).catch(res => {
            this._route.error(res.data);
        });
    }

    getQuizTime(seconds) {
        let minutes = Math.ceil(seconds / 60);
        return minutes;
    }
}