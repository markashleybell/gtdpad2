<template>
    <div class="section">
        <h2>{{ title }}</h2>
        <input type="text" v-model="updatedTitle" />
        <button @click="updateTitle">Save</button>
        <div v-if="richtextblock">RICH TEXT BLOCK</div>
        <div v-if="imageblock">IMAGE BLOCK</div>
        <div v-if="list"><List :list="section" /></div>
    </div>
</template>

<script lang="ts">
import { defineComponent, PropType } from 'vue';

import List from './List.vue';

import { ISection, SectionType } from '../core/Domain';

interface ComponentData {
    title: string;
    updatedTitle: string;
    type: SectionType;
}

export default defineComponent({
    components: {
        List
    },
    props: {
        section: { type: Object as PropType<ISection>, required: true }
    },
    data(): ComponentData {
        return {
            title: this.section.title,
            updatedTitle: this.section.title,
            type: this.section.type
        };
    },
    methods: {
        updateTitle() {
            console.log('PERSIST SECTION TITLE ' + this.section.id);
            this.title = this.updatedTitle;
            this.$emit('title-updated', this.section.id, this.title);
        }
    },
    computed: {
        richtextblock(): boolean {
            return this.type == SectionType.RichTextBlock;
        },
        imageblock(): boolean {
            return this.type == SectionType.ImageBlock;
        },
        list(): boolean {
            return this.type == SectionType.List;
        }
    }
});
</script>
