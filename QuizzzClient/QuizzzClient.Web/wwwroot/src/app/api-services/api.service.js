export default class ApiService {
    /** @ngInject */
    constructor (apiPath, Upload) {
        this.apiPath = apiPath;
        this.Upload = Upload;
    }

    addQuizz(file) {
        return this.Upload.upload({
            url: `${this.apiPath}`,
            data: {
                file: file
            }
        })
    }
}