<template>
    <ul v-if="items">
        <ListItem
            v-for="item in items"
            :key="item.id"
            :item="item"
            @text-updated="updateItemText"
        />
    </ul>
</template>

<script lang="ts">
import { defineComponent, PropType } from 'vue';

import ListItem from './ListItem.vue';

import { IList, IListItem } from '../core/Domain';

interface ComponentData {
    title: string;
    items: IListItem[];
}

export default defineComponent({
    components: {
        ListItem
    },
    props: {
        list: { type: Object as PropType<IList>, required: true }
    },
    data(): ComponentData {
        return {
            title: '',
            items: []
        };
    },
    methods: {
        async updateItemText(id: string, text: string) {
            const item = this.items.find(i => i.id == id);

            if (item) {
                item.text = text;
            }
        }
    },
    async created() {
        this.items = [
            { id: 'xxx1', order: 100, text: 'Item 1' },
            { id: 'xxx2', order: 200, text: 'Item 2' },
            { id: 'xxx3', order: 300, text: 'Item 3' }
        ];

        // this.items = await this.$api.httpGet<IListItem[]>('/pages');
    }
});
</script>
