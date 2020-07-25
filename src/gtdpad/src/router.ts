import Vue from 'vue';
import Router from 'vue-router';
import Login from './components/Login.vue';
import Page from './components/Page.vue';

Vue.use(Router);

export default new Router({
    mode: 'history',
    routes: [
        {
            path: '/users/login',
            name: 'login',
            component: Login
        },
        {
            path: '/:id?',
            name: 'page',
            component: Page,
            props: true
        }
    ]
});
