<template>
    <div class="row">
        <div class="col-8" id="content">
            <Page :id="id" />
        </div>
        <nav class="col-4" id="navigation">
            <PageNavigation :pages="pages" />
        </nav>
    </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';

import Page from '@/components/Page.vue';
import PageNavigation from '@/components/PageNavigation.vue';

import { IPage } from '@/core/Domain';

interface ComponentData {
    pages: IPage[];
}

export default defineComponent({
    components: {
        Page,
        PageNavigation
    },
    props: {
        id: { type: String }
    },
    data(): ComponentData {
        return {
            pages: []
        };
    },
    async created() {
        this.pages = await this.$api.httpGet<IPage[]>('/pages');
    }
});
</script>
