import App from './app.directive';
import routeConfig from './app.routes';

angular.module('app', [ 'ui.router' ]);

angular.module('app')
    .config(routeConfig)
    .directive('app', () => new App());