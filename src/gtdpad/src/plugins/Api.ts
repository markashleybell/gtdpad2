import _Vue from 'vue';
import { HttpClient } from './HttpClient';

export function Api(Vue: typeof _Vue, options?: any): void {
    Vue.prototype.$api = new HttpClient('/api', null);
}
