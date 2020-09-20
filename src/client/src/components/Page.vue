<template>
    <div v-if="page">
        <h1>{{ page.title }}</h1>
        <input type="text" v-model="updatedTitle" />
        <button @click="updateTitle">Save</button>
        <Section
            v-for="section in sections"
            :key="section.id"
            :section="section"
        />
    </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';

import Section from '@/components/Section.vue';

import { EmptyPage, IPage, ISection, SectionType } from '@/core/Domain';

interface ComponentData {
    page: IPage;
    updatedTitle: string;
    sections: ISection[];
}

export default defineComponent({
    components: {
        Section
    },
    props: {
        id: { type: String }
    },
    data(): ComponentData {
        return {
            page: EmptyPage,
            sections: [] as ISection[],
            updatedTitle: ''
        };
    },
    methods: {
        async loadContent() {
            // TODO: Could initially load all sections/items in the same call?

            if (this.id) {
                this.page = await this.$api.httpGet<IPage>(`/pages/${this.id}`);
            } else {
                const pages = await this.$api.httpGet<IPage[]>('/pages');
                this.page = pages[0];
            }

            this.updatedTitle = this.page.title;

            // The endpoint is actually returning IList[]...
            const lists = await this.$api.httpGet<ISection[]>(
                `/pages/${this.page.id}/lists`
            );

            // This will be done in the API eventually, just experimenting
            lists.forEach((l: ISection) => (l.type = SectionType.List));

            this.sections.length = 0;

            this.sections.push(...lists);
        },
        updateTitle() {
            console.log('PERSIST PAGE TITLE ' + this.page.id);
            this.page.title = this.updatedTitle;
            this.$emit('title-updated', this.page.id, this.page.title);
        }
    },
    async mounted() {
        await this.loadContent();
    },
    watch: {
        async $route(to, from) {
            await this.loadContent();
        },
        async id() {
            await this.loadContent();
        }
    }
});
</script>
