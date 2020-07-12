<template>
    <ul>
        <PageNavigationItem v-for="page in pages" :key="page.id" :page="page" @click="itemClicked" />
    </ul>
</template>

<script lang="ts">
    import Vue, { PropType } from 'vue';
    import PageNavigationItem from './PageNavigationItem.vue'

    import { IPage } from '../core/Domain';

    export default Vue.extend({
        components: {
            PageNavigationItem
        },
        props: {
            pages: { type: Array as PropType<IPage[]>, required: true }
        },
        methods: {
            itemClicked(e: Event, id: string) {
                e.preventDefault();
                this.$emit('item-click', id);
            }
        }
    });
</script>
