import App from './app.directive';

angular.module('app', []);

angular.module('app')
    .directive('app', () => new App());