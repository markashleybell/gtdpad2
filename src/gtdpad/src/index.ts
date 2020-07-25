import Vue from 'vue';
import { Api } from './plugins/Api';

Vue.use(Api);

import App from './App.vue';
import router from './router';

new Vue({
    router,
    render: createElement => createElement(App),
}).$mount('#app');
