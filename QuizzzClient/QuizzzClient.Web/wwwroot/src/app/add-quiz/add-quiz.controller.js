export default class AddQuizCtrl {
    /** @ngInject */
    constructor (api, route) {
        this.api = api;

        route.currentPageTitle = 'Add quiz';

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