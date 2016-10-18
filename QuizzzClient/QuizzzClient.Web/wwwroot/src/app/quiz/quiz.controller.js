import moment from '../../../lib/moment/moment';

export default class QuizCtrl {
    /** @ngInject */
    constructor(api, $stateParams, route, storageService, $interval, $scope) {
        this.api = api;
        this._route = route;
        this._storageService = storageService;
        this._$interval = $interval;

        this.id = $stateParams.id;
        this.quiestions = [];
        this.results = [];
        this.currentQuestion = {};
        this.leftoverTime = moment.duration(0);
        this.countdown = null;

        // Undo countdown after exiting controller
        $scope.$on('$destroy', () => {
            if (this.countdown !== null) {
                this._$interval.cancel(this.countdown);
            }
        });

        // Add source for saving quiz state
        this._initStorage();

        let isNewQuiz = (!storageService.settingsSaved || storageService.savedData.id !== this.id);
        if (!isNewQuiz) {
            this._restoreData(storageService.savedData);
        }

        this._startQuiz(isNewQuiz);
    }

    skipQuestion() {
        if (!this.currentQuestion.wasSkipped) {
            this.currentQuestion.wasSkipped = true;
            this.questions.push(this.currentQuestion);
            this.nextQuestion();
        }
    }

    nextQuestion() {
        if (this.currentQuestion.id) {
            this._saveQuestionAnswers();
        }

        let nextQuestion = this.questions.shift();

        if (nextQuestion) {
            this.currentQuestion = nextQuestion;
        } else {
            let sendData = {
                QuizId: this.id,
                Answers: this.results
            };

            this.api.acceptQuiz(sendData).then(res => {
                this._storageService.removeAllSources();
                this._route.go('quizResults', 'Results', {
                    result: res.data
                });
            }).catch(res => {
                this._route.error(res.data);
            });
        }
    }

    getLeftoverTime() {
        let formatted = ("0" + Math.floor(this.leftoverTime.asHours())).slice(-2) +
            moment.utc(this.leftoverTime.asMilliseconds()).format(":mm:ss")
        return formatted;
    }

    _startQuiz(isNewQuiz) {
        this.api.getQuiz(this.id).then(res => {
            this.questions = res.data.questions.map(q => {
                q.answers = q.answers.map(a => {
                    return {
                        answerBody: a,
                        isChecked: false
                    };
                });
                return q;
            });

            if (isNewQuiz) {
                this.leftoverTime = moment.duration(res.data.time, 's');
                this.nextQuestion();
            };

            // Start countdown
            this.countdown = this._$interval(() => {
                this.leftoverTime.subtract(1, 's');
                if (this.leftoverTime <= 0) {
                    this._$interval.cancel(this.countdown);
                    this._timeOver();
                }
            }, 1000)

            // Change toolbar heading
            this._route.currentPageTitle = res.data.name;
        })
        .catch((res) => {
            this._route.error(res.data);
        });
    }

    _restoreData(savedData) {
        this.id = savedData.id;
        this.results = savedData.results;
        this.currentQuestion = savedData.currentQuestion;
        this.leftoverTime = moment.duration(savedData.leftoverTime, 's');
    }

    _saveQuestionAnswers() {
        this.results[this.currentQuestion.id - 1] = [];
        let questionResults = this.results[this.currentQuestion.id - 1];

        this.currentQuestion.answers.forEach((answer, i) => {
            if (answer.isChecked) {
                questionResults.push(i);
            }
        });
    }

    _initStorage() {
        this._storageService.removeAllSources();
        this._storageService.addSource(() => {
            return {
                id: this.id,
                results: this.results,
                currentQuestion: this.currentQuestion,
                leftoverTime: this.leftoverTime.asSeconds()
            }
        });
    }

    _timeOver() {
        this._route.go('timeover', 'Time is over', {
            id: this.id
        });
        console.log('time over');
    }
}