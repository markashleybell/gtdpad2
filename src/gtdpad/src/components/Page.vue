<template>
    <div>
        <h1>{{ page.title }}</h1>
        <input type="text" v-model="updatedTitle" />
        <button @click="updateTitle">Save</button>
        <Section v-for="section in sections" :key="section.id" :section="section" :http-client="httpClient" />
    </div>
</template>

<script lang="ts">
    import Vue, { PropType } from 'vue';
    import Section from './Section.vue'

    import { IHttpClient } from '../core/IHttpClient';
    import { IPage, ISection, SectionType } from '../core/Domain';

    interface ComponentData {
        updatedTitle: string;
        sections: ISection[];
    }

    export default Vue.extend({
        components: {
            Section
        },
        props: {
            httpClient: { type: Object as PropType<IHttpClient>, required: true },
            page: { type: Object as PropType<IPage>, required: true }
        },
        data(): ComponentData {
            return {
                sections: [] as ISection[],
                updatedTitle: ''
            }
        },
        methods: {
            updateTitle() {
                console.log('PERSIST PAGE TITLE ' + this.page.id);
                this.page.title = this.updatedTitle;
                this.$emit('title-updated', this.page.id, this.page.title);
            }
        },
        watch: {
            async page() {
                this.updatedTitle = this.page.title;

                // Load page data here
                // Could initially load all sections/items in the same call?

                // The endpoint is actually returning IList[]...
                const lists = await this.httpClient.httpGet<ISection[]>(`/pages/${this.page.id}/lists`);

                // This will be done in the API eventually, just experimenting
                lists.forEach(l => l.type = SectionType.List);

                this.sections.length = 0;

                this.sections.push(...lists);
            }
        }
    });
</script>
