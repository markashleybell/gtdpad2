<template>
    <div class="row">
        <div class="col-8" id="content">
            <Page v-if="page" :page="page" :http-client="httpClient" @title-updated="updatePageTitle" />
            <div v-else>
                Loading...
            </div>
        </div>
        <nav class="col-4" id="navigation">
            <PageNavigation :pages="pages" @item-click="loadPage" />
        </nav>
    </div>
</template>

<script lang="ts">
    import Vue from 'vue';

    import Page from './components/Page.vue';
    import PageNavigation from './components/PageNavigation.vue';

    import { HttpClient } from './core/HttpClient';
    import { IHttpClient } from './core/IHttpClient';
    import { IPage } from './core/Domain';

    const token = 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9zaWQiOiJkZjc3Nzc4Zi0yZWYzLTQ5YWYtYTFhOC1iMWYwNjQ4OTFlZjUiLCJlbWFpbCI6InRlc3R1c2VyQGd0ZHBhZC5jb20iLCJyb2xlIjoiTWVtYmVyIiwibmJmIjoxNTk1NjU3MTM5LCJleHAiOjE1OTU3NDM1MzksImlhdCI6MTU5NTY1NzEzOX0.bLKMXQKBjRjeWTm7B1LUprpi3TKN9otajNKXxdRBrDc';

    interface ComponentData {
        httpClient: IHttpClient;
        pages: IPage[];
        page: IPage | null;
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
                page: null
            }
        },
        methods: {
            async loadPage(id: string) {
                this.page = this.pages.find(p => p.id == id) || null;
            },
            async updatePageTitle(id: string, title: string) {
                const page = this.pages.find(p => p.id == id);

                if (page) {
                    page.title = title;
                }
            }
        },
        async created() {
            try {
                this.pages = await this.httpClient.httpGet<IPage[]>('/pages');
                this.page = this.pages.length ? this.pages[0] : null;
            } catch (e) {
                if (e.status === 401) {
                    alert('LOGIN');
                } else {
                    console.log(e);
                }
            }
        }
    });
</script>
