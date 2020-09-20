import { App } from 'vue';
import { HttpClient } from './HttpClient';

export default {
    install: (app: App) => {
        app.config.globalProperties.$api = new HttpClient(
            'https://localhost:5001/api'
        );
    }
};
