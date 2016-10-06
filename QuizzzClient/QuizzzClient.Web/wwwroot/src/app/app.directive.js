let templateUrl = require('ngtemplate!html!./app.html');

export default class App {
    /** @ngInject */
    constructor() {
        this.templateUrl = templateUrl;
    }
}