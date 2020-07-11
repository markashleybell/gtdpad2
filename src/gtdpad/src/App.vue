<template>
    <div class="row">
        <div class="col-8" id="content">
            <PageContent :http-client="httpClient" :id="pageID" />
        </div>
        <nav class="col-4" id="navigation">
            <PageNavigation :http-client="httpClient" @item-click="loadPageContent" />
        </nav>
    </div>
</template>

<script lang="ts">
    import Vue from "vue";
    import 'bootstrap';

    import PageContent from './components/PageContent.vue';
    import PageNavigation from './components/PageNavigation.vue';

    import { HttpClient } from './core/HttpClient';
    import { IHttpClient } from "./core/IHttpClient";

    const token = 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9zaWQiOiJkZjc3Nzc4Zi0yZWYzLTQ5YWYtYTFhOC1iMWYwNjQ4OTFlZjUiLCJlbWFpbCI6InRlc3R1c2VyQGd0ZHBhZC5jb20iLCJyb2xlIjoiTWVtYmVyIiwibmJmIjoxNTk0NDQ4MTM4LCJleHAiOjE1OTUwNTI5MzgsImlhdCI6MTU5NDQ0ODEzOH0.RQky8HIZDMT-ICmDnWl2lewWgMxQCL-CADQZyZk5dXg';

    interface ComponentData {
        httpClient: IHttpClient;
        pageID: string;
    }

    export default Vue.extend({
        components: {
            PageContent,
            PageNavigation
        },
        data: function (): ComponentData {
            return {
                httpClient: new HttpClient('/api', token),
                pageID: ''
            }
        },
        methods: {
            async loadPageContent(id: string) {
                this.pageID = id;
            }
        }
    });
</script>
