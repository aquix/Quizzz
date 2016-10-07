let allQuizzesTemplateUrl = require('ngtemplate!html!./all-quizzes/all-quizzes.html');
let quizzTemplateUrl = require('ngtemplate!html!./quizz/quizz.html');
let addQuizzTemplateUrl = require('ngtemplate!html!./add-quizz/add-quizz.html');
let statsTemplateUrl = require('ngtemplate!html!./stats/stats.html');
let resultsTemplateUrl = require('ngtemplate!html!./quizz-results/quizz-results.html');

/** @ngInject */
export default function ($stateProvider, $urlRouterProvider) {

    $urlRouterProvider.otherwise('/');

    $stateProvider
        .state('index', {
            url: '/',
            templateUrl: allQuizzesTemplateUrl,
            controller: 'AllQuizzesCtrl as ctrl'
        })
        .state('quizz', {
            url: '/quizz/:id',
            templateUrl: quizzTemplateUrl,
            controller: 'QuizzCtrl as ctrl'
        })
        .state('addQuizz', {
            url: '/addQuizz',
            templateUrl: addQuizzTemplateUrl,
            controller: 'AddQuizzCtrl as ctrl'
        })
        .state('stats', {
            url: '/stats',
            templateUrl: statsTemplateUrl,
            controller: 'StatsCtrl as ctrl'
        })
        .state('quizzResults', {
            url: '/results',
            templateUrl: resultsTemplateUrl,
            controller: 'QuizzResultsCtrl as ctrl'
        });

}