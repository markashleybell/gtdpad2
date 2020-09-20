import { createRouter, createWebHistory, RouteRecordRaw } from 'vue-router';

import Login from '@/views/Login.vue';
import Main from '@/views/Main.vue';

const routes: Array<RouteRecordRaw> = [
    {
        path: '/login',
        name: 'login',
        component: Login
    },
    {
        path: '/:id?',
        name: 'page',
        component: Main,
        props: true
    }
];

const router = createRouter({
    history: createWebHistory(process.env.BASE_URL),
    routes
});

export default router;
