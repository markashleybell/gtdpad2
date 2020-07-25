import { IHttpClient } from './IHttpClient';

declare module 'vue/types/vue' {
    interface Vue {
        $api: IHttpClient;
    }
}
