import { createApp } from 'vue';
import app from './App.vue';
import './registerServiceWorker';
import router from './router';

import api from './plugins/Api';

createApp(app)
    .use(router)
    .use(api)
    .mount('#app');
