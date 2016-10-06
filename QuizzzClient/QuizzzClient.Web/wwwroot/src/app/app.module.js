import routeConfig from './app.routes';
import App from './app.directive';
import AddQuizzCtrl from './add-quizz/add-quizz.controller';
import AllQuizzesCtrl from './all-quizzes/all-quizzes.controller';
import PopularQuizzesCtrl from './popular-quizzes/popular-quizzes.controller';
import QuizzCtrl from './quizz/quizz.controller';
import StatsCtrl from './stats/stats.controller';

angular.module('app', [ 'ui.router' ]);

angular.module('app')
    .config(routeConfig)
    .directive('app', () => new App())
    .controller('AddQuizzCtrl', AddQuizzCtrl)
    .controller('AllQuizzesCtrl', AllQuizzesCtrl)
    .controller('PopularQuizzesCtrl', PopularQuizzesCtrl)
    .controller('QuizzCtrl', QuizzCtrl)
    .controller('StatsCtrl', StatsCtrl);