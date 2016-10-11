export default class StorageService {
    constructor(localStorageService, $state) {
        this.savedData = {};
        this.settingsSaved = false;

        this._$state = $state;
        this._storage = localStorageService;
        this._sources = [];


        localStorageService.setStorageType('sessionStorage');

        // Restore saved settings if they are
        this.settingsSaved = this._storage.get('settingsSaved');

        if (!this.settingsSaved) {
            return;
        }

        localStorageService.remove('settingsSaved');
        let keys = localStorageService.keys();

        for(let key of keys) {
            let value = localStorageService.get(key);
            this.savedData[key] = JSON.parse(value);
        }

        localStorageService.clearAll();
    }

    // source is function which returns object(key: value)
    addSource(source) {
        this._sources.push(source);
    }

    removeAllSources() {
        this._sources = [];
    }

    save() {
        if (this._$state.current.name === 'quiz') {
            for(let source of this._sources) {
                let data = source();
                for(let key of Object.keys(data)) {
                    this._storage.set(key, JSON.stringify(data[key]));
                }
            }

            this._storage.set('settingsSaved', true);
        }
    }
}