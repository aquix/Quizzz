function MainViewModel() {
    var self = this;

    self.quizz = ko.observable({});
    self.outputType = ko.observable('json');

    self.generateQuizz = function () {
        // Assign ids
        self.quizz().questions().forEach(function (question, i) {
            question.id = i + 1;
        });

        var jsonData = ko.toJSON(self);
        location = '/generate?jsonData=' + jsonData;
    }
}

function QuizzViewModel() {
    var self = this;

    self.author = ko.observable('');
    self.category = ko.observable('');
    self.questions = ko.observableArray([]);

    // init
    self.questions.push(new QuestionViewModel());

    self.addQuestion = function () {
        self.questions.push(new QuestionViewModel());
    }

    self.removeQuestion = function (question) {
        self.questions.remove(question);
    }
}

function QuestionViewModel() {
    var self = this;

    self.id = 0;
    self.questionBody = ko.observable('');
    self.answers = ko.observableArray([]);

    // init
    self.answers.push(new AnswerViewModel());
    self.answers.push(new AnswerViewModel());

    self.addAnswer = function () {
        self.answers.push(new AnswerViewModel());
    }

    self.removeAnswer = function (answer) {
        self.answers.remove(answer);
    }
}

function AnswerViewModel() {
    var self = this;

    self.isCorrect = ko.observable(false);
    self.answerString = ko.observable('');
}

function init() {
    var mainViewModel = new MainViewModel();
    mainViewModel.quizz(new QuizzViewModel());
    ko.applyBindings(mainViewModel);
}

init();