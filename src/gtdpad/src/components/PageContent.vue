<template>
    <div id="page">
        <h1>{{ page.title }}</h1>
    </div>
</template>

<script lang="ts">
    import Vue, { PropType } from "vue";

    import { IHttpClient } from '../core/IHttpClient';
    import { IPage } from '../core/IPage';

    interface ComponentData {
        page: IPage;
    }

    export default Vue.extend({
        props: {
            httpClient: Object as PropType<IHttpClient>,
            id: String
        },
        data(): ComponentData {
            return {
                page: {} as IPage
            }
        },
        watch: {
            async id() {
                this.page = await this.httpClient.httpGet<IPage>(`/pages/${this.id}`);
            }
        }
    });
</script>
