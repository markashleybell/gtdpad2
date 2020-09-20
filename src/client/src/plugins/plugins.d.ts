import { IHttpClient } from './IHttpClient';

declare module '@vue/runtime-core' {
    export interface ComponentCustomProperties {
        $api: IHttpClient;
        $route: RouteLocationNormalizedLoaded;
        $router: Router;
    }
}
