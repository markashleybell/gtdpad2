<template>
    <div class="row">
        <div class="col-8" id="content">
            <router-view />
        </div>
        <nav class="col-4" id="navigation">
            <PageNavigation :pages="pages" />
        </nav>
    </div>
</template>

<script lang="ts">
    import Vue from 'vue';

    import Page from './components/Page.vue';
    import PageNavigation from './components/PageNavigation.vue';

    import { IPage } from './core/Domain';

    interface ComponentData {
        pages: IPage[];
    }

    export default Vue.extend({
        components: {
            Page,
            PageNavigation
        },
        data(): ComponentData {
            return {
                pages: []
            }
        },
        async created() {
            try {
                this.pages = await this.$api.httpGet<IPage[]>('/pages');
            } catch (e) {
                if (e.status === 401) {
                    this.$router.push({ name: 'login' });
                } else {
                    console.log(e);
                }
            }
        }
    });
</script>
