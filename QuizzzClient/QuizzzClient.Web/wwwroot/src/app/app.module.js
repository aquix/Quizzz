import routeConfig from './app.routes';
import App from './app.directive';
import ApiService from './api-services/api.service';
import AddQuizzCtrl from './add-quizz/add-quizz.controller';
import AllQuizzesCtrl from './all-quizzes/all-quizzes.controller';
import PopularQuizzesCtrl from './popular-quizzes/popular-quizzes.controller';
import QuizzCtrl from './quizz/quizz.controller';
import StatsCtrl from './stats/stats.controller';

angular.module('app', [ 'ui.router', 'ngFileUpload' ]);

angular.module('app')
    .config(routeConfig)
    .constant('apiPath', '/api/quizz')
    .directive('app', () => new App())
    .service('api', ApiService)
    .controller('AddQuizzCtrl', AddQuizzCtrl)
    .controller('AllQuizzesCtrl', AllQuizzesCtrl)
    .controller('PopularQuizzesCtrl', PopularQuizzesCtrl)
    .controller('QuizzCtrl', QuizzCtrl)
    .controller('StatsCtrl', StatsCtrl);