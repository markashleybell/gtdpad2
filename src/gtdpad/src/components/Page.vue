<template>
    <div v-if="!page">
        <h1>Loading...</h1>
    </div>
    <div v-else>
        <h1>{{ page.title }}</h1>
        <input type="text" v-model="updatedTitle" />
        <button @click="$emit('title-updated', page.id, updatedTitle)">Save</button>
    </div>
</template>

<script lang="ts">
    import Vue, { PropType } from "vue";

    import { IHttpClient } from "../core/IHttpClient";
    import { IPage } from '../core/IPage';

    interface ComponentData {
        updatedTitle: string;
    }

    export default Vue.extend({
        props: {
            httpClient: Object as PropType<IHttpClient>,
            page: Object as PropType<IPage>
        },
        data(): ComponentData {
            return {
                updatedTitle: ''
            }
        },
        watch: {
            async page() {
                this.updatedTitle = this.page.title;

                // Load all page data here
                const lists = await this.httpClient.httpGet<IPage[]>(`/pages/${this.page.id}/lists`);
                // console.log(lists);
            }
        }
    });
</script>
