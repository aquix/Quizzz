export default class AllQuizzesCtrl {
    /** @ngInject */
    constructor(api, route, $stateParams) {
        this._route = route;
        this.api = api;

        this.COUNT_OF_QUIZZES = 2;
        this.quizzes = [];
        this.categories = [];
        this.selectedCategory = "";
        this.currentPage = 1;
        this.totalPages = 0;

        this.loadFromServer();
    }

    openQuiz(id) {
        this._route.go('quiz', id, { id: id });
    }

    filterCategory() {
        this.currentPage = 1;
        this.loadFromServer();
    }

    loadFromServer() {
        let startIndex = (this.currentPage - 1) * this.COUNT_OF_QUIZZES;
        this.api.getPreviews(this.COUNT_OF_QUIZZES, startIndex, this.selectedCategory).then(res => {
            this.quizzes = res.data.quizzes;
            this.categories = res.data.categories;

            this.totalPages = res.data.totalPages;
        }).catch(res => {
            this._route.error(res.data);
        });
    }

    getQuizTime(seconds) {
        let minutes = Math.ceil(seconds / 60);
        return minutes;
    }

    onPageChanged() {
        console.log('change');
    }
}