import routeConfig from './app.routes';
import App from './app.directive';
import ApiService from './api-services/api.service';
import AddQuizCtrl from './add-quiz/add-quiz.controller';
import AllQuizzesCtrl from './all-quizzes/all-quizzes.controller';
import QuizCtrl from './quiz/quiz.controller';
import StatsCtrl from './stats/stats.controller';
import QuizResultsCtrl from './quiz-results/quiz-results.controller';
import StorageService from './utils/storage.service';

angular.module('app', [ 'ui.router', 'ngFileUpload', 'LocalStorageModule' ]);

angular.module('app')
    .config(routeConfig)
    .constant('apiPath', '/api/quiz')
    .directive('app', () => new App())
    .service('api', ApiService)
    .service('storageService', StorageService)
    .controller('AddQuizCtrl', AddQuizCtrl)
    .controller('AllQuizzesCtrl', AllQuizzesCtrl)
    .controller('QuizCtrl', QuizCtrl)
    .controller('StatsCtrl', StatsCtrl)
    .controller('QuizResultsCtrl', QuizResultsCtrl);

angular.module('app')
    .run(function ($window, storageService) {
        window.onbeforeunload = function () {
            console.log('close window');
            storageService.save();
            // handle the exit event
        };
    });