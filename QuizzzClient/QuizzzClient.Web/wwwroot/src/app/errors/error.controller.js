export default class ErrorCtrl {
    constructor($stateParams) {
        this.message = $stateParams.message;
    }
}