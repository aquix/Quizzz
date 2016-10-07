let allQuizzesTemplateUrl = require('ngtemplate!html!./all-quizzes/all-quizzes.html');
let quizTemplateUrl = require('ngtemplate!html!./quiz/quiz.html');
let addQuizTemplateUrl = require('ngtemplate!html!./add-quiz/add-quiz.html');
let statsTemplateUrl = require('ngtemplate!html!./stats/stats.html');
let resultsTemplateUrl = require('ngtemplate!html!./quiz-results/quiz-results.html');

/** @ngInject */
export default function ($stateProvider, $urlRouterProvider) {

    $urlRouterProvider.otherwise('/');

    $stateProvider
        .state('index', {
            url: '/',
            templateUrl: allQuizzesTemplateUrl,
            controller: 'AllQuizzesCtrl as ctrl'
        })
        .state('quiz', {
            url: '/quiz/:id',
            templateUrl: quizTemplateUrl,
            controller: 'QuizCtrl as ctrl'
        })
        .state('addQuiz', {
            url: '/addQuiz',
            templateUrl: addQuizTemplateUrl,
            controller: 'AddQuizCtrl as ctrl'
        })
        .state('stats', {
            url: '/stats',
            templateUrl: statsTemplateUrl,
            controller: 'StatsCtrl as ctrl'
        })
        .state('quizResults', {
            url: '/results',
            templateUrl: resultsTemplateUrl,
            controller: 'QuizResultsCtrl as ctrl'
        });

}