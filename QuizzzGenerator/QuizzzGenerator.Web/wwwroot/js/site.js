function MainViewModel() {
    var self = this;

    self.quizz = ko.observable({});
    self.outputType = ko.observable('json');

    self.generateQuizz = function () {
        var isValid = validate(self);
        if (!isValid) {
            return;
        }

        // Assign ids
        self.quizz().questions().forEach(function (question, i) {
            question.id = i + 1;
        });

        var jsonData = ko.toJSON(self, function (key, value) {
            if (key === "errors") {
                return undefined;
            }
            return value;
        });

        var headers = new Headers();
        headers.append('Content-Type', 'application/json');
        fetch('/api/generate', {
            method: 'post',
            headers: headers,
            body: jsonData
        })
        .then(function (response) {
            response.text().then(function (text) {
                document.querySelector('html').textContent = text;
            });
        });
    };

    self.errors = ko.observableArray();
}

function QuizzViewModel() {
    var self = this;

    self.author = ko.observable('');
    self.name = ko.observable('');
    self.category = ko.observable('');
    self.questions = ko.observableArray([]);

    // init
    self.questions.push(new QuestionViewModel());

    self.addQuestion = function () {
        self.questions.push(new QuestionViewModel());
    };

    self.removeQuestion = function (question) {
        self.questions.remove(question);
    };
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
    };

    self.removeAnswer = function (answer) {
        self.answers.remove(answer);
    };
}

function AnswerViewModel() {
    var self = this;

    self.isCorrect = ko.observable(false);
    self.answerBody = ko.observable('');
}

function init() {
    var mainViewModel = new MainViewModel();
    mainViewModel.quizz(new QuizzViewModel());
    ko.applyBindings(mainViewModel);
}

function validate(mainViewModel) {
    function addError(err) {
        mainViewModel.errors.push(err);
    }

    mainViewModel.errors([]);
    var quizz = mainViewModel.quizz();

    if (quizz.author() === '') {
        addError('Author field is required');
    }
    if (quizz.category() === '') {
        addError('Category field is required');
    }
    if (quizz.name() === '') {
        addError('Name is required');
    }
    if (quizz.questions().length === 0) {
        addError('Quizz must contain at least one question');
    }
    quizz.questions().forEach(function (question, index) {
        if (question.questionBody() === '') {
            addError('Question ' + (index + 1) + ': body is empty');
        }

        if (question.answers().length < 2) {
            addError('Question ' + (index + 1) + ' must have at least two answers');
        }

        var areAllAnswersIncorrect = true;
        var areAllAnswersCorrect = true;
        var isAnyAnswerEmpty = false;

        question.answers().forEach(function (answer) {
            if (answer.isCorrect()) {
                areAllAnswersIncorrect = false;
            } else {
                areAllAnswersCorrect = false;
            }

            if (answer.answerString() === '') {
                isAnyAnswerEmpty = true;
            }
        });

        if (areAllAnswersCorrect) {
            addError('Question ' + (index + 1) + ': all answers can\'t be correct');
        } else if (areAllAnswersIncorrect) {
            addError('Question ' + (index + 1) + ': at least one answer should be correct');
        }

        if (isAnyAnswerEmpty) {
            addError('Question ' + (index + 1) + ': all answers shouldn\'t be empty strings');
        }
    });

    if (mainViewModel.errors().length === 0) {
        return true;
    } else {
        return false;
    }
}

init();