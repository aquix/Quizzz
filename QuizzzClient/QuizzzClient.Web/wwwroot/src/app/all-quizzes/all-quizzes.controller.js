export default class AllQuizzesCtrl {
    /** @ngInject */
    constructor(api, $state) {
        this.$state = $state;

        const COUNT_OF_QUIZZES = 0;
        this.quizzes = [];

        api.getPreviews(COUNT_OF_QUIZZES)
            .then(res => {
                this.quizzes = res.data;
            });
    }

    openQuiz(id) {
        this.$state.go('quiz', { id: id });
    }
}