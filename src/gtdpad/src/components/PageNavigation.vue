<template>
    <ul>
        <PageNavigationItem v-for="page in pages" :key="page.id" :page="page" @click="itemClicked" />
    </ul>
</template>

<script lang="ts">
    import Vue, { PropType } from "vue";
    import PageNavigationItem from './PageNavigationItem.vue'

    import { IHttpClient } from '../core/IHttpClient';
    import { IPage } from '../core/IPage';

    interface ComponentData {
        pages: IPage[];
    }

    export default Vue.extend({
        components: {
            PageNavigationItem
        },
        props: {
            httpClient: Object as PropType<IHttpClient>
        },
        data(): ComponentData {
            return {
                pages: []
            }
        },
        methods: {
            async loadData() {
                this.pages = await this.httpClient.httpGet<IPage[]>('/pages');
            },
            async itemClicked(e: Event, id: string) {
                e.preventDefault();
                this.$emit('item-click', id);
            }
        },
        async created() {
            this.loadData();
        }
    });
</script>
