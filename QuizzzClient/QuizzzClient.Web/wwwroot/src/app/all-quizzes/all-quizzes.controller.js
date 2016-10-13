export default class AllQuizzesCtrl {
    /** @ngInject */
    constructor(api, $state, $stateParams) {
        this.$state = $state;
        this.api = api;

        const COUNT_OF_QUIZZES = $stateParams.count;
        this.quizzes = [];
        this.categories = [];
        this.selectedCategory = "";

        this.api.getPreviews(COUNT_OF_QUIZZES)
            .then(res => {
                this.quizzes = res.data.quizzes;
                this.categories = res.data.categories;
            });
    }

    openQuiz(id) {
        this.$state.go('quiz', { id: id });
    }

    filterCategory() {
        this.api.getPreviews(0, this.selectedCategory)
            .then(res => {
                this.quizzes = res.data.quizzes;
                this.categories = res.data.categories;
            });
    }

    getQuizTime(questionsCount) {
        const TIME_PER_QUESTION = 20;
        let seconds = questionsCount * TIME_PER_QUESTION;
        let minutes = Math.ceil(seconds / 60);
        return minutes;
    }
}