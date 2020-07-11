<template>
    <div class="row">
        <div class="col-8" id="content">
            <Page :page="page" :http-client="httpClient" @title-updated="updatePageTitle" />
        </div>
        <nav class="col-4" id="navigation">
            <PageNavigation :pages="pages" @item-click="loadContent" />
        </nav>
    </div>
</template>

<script lang="ts">
    import Vue from "vue";

    // TODO: Do we actually need any of the Bootstrap JS?
    // import 'bootstrap';

    import Page from './components/Page.vue';
    import PageNavigation from './components/PageNavigation.vue';

    import { HttpClient } from './core/HttpClient';
    import { IHttpClient } from "./core/IHttpClient";
    import { IPage } from './core/IPage';

    const token = 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9zaWQiOiJkZjc3Nzc4Zi0yZWYzLTQ5YWYtYTFhOC1iMWYwNjQ4OTFlZjUiLCJlbWFpbCI6InRlc3R1c2VyQGd0ZHBhZC5jb20iLCJyb2xlIjoiTWVtYmVyIiwibmJmIjoxNTk0NDQ4MTM4LCJleHAiOjE1OTUwNTI5MzgsImlhdCI6MTU5NDQ0ODEzOH0.RQky8HIZDMT-ICmDnWl2lewWgMxQCL-CADQZyZk5dXg';

    interface ComponentData {
        httpClient: IHttpClient;
        pages: IPage[];
        page: IPage | undefined;
    }

    export default Vue.extend({
        components: {
            Page,
            PageNavigation
        },
        data(): ComponentData {
            return {
                httpClient: new HttpClient('/api', token),
                pages: [],
                page: undefined
            }
        },
        methods: {
            async loadContent(id: string) {
                this.page = this.pages.find(p => p.id == id);
            },
            async updatePageTitle(id: string, title: string) {
                const page = this.pages.find(p => p.id == id);

                if (page) {
                    page.title = title;
                    console.log('UPDATE PAGE API CALL: ' + id);
                }
            }
        },
        async created() {
            this.pages = await this.httpClient.httpGet<IPage[]>('/pages');
            this.page = this.pages.length ? this.pages[0] : undefined;
        }
    });
</script>
