import AppCtrl from './app.controller';

let templateUrl = require('ngtemplate!html!./app.html');


export default class App {
    /** @ngInject */
    constructor() {
        this.templateUrl = templateUrl;
        this.controller = AppCtrl;
        this.controllerAs = 'appCtrl';
    }
}