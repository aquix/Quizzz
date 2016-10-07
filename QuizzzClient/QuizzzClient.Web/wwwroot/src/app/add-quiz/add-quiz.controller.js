export default class AddQuizCtrl {
    /** @ngInject */
    constructor (api) {
        this.api = api;

        this.file = null;
        this.status = '';
    }

    upload() {
        this.api.addQuiz(this.file)
            .then(() => {
                this.status = 'success';
            })
            .catch(() => {
                this.status = 'error';
            });
    }
}