export default class AllQuizzesCtrl {
    /** @ngInject */
    constructor(api, $state) {
        this.$state = $state;

        const COUNT_OF_QUIZZES = 0;
        this.quizzes = [];

        api.getPopular(COUNT_OF_QUIZZES)
            .then(res => {
                this.quizzes = res.data;
            });
    }

    openQuizz(id) {
        this.$state.go('quizz', { id: id });
    }
}