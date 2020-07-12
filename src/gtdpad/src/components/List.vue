<template>
    <ul v-if="items">
        <ListItem v-for="item in items" :key="item.id" :item="item" @text-updated="updateItemText" />
    </ul>
</template>

<script lang="ts">
    import Vue, { PropType } from 'vue';
    import ListItem from './ListItem.vue';

    import { IHttpClient } from '../core/IHttpClient';
    import { IList, IListItem } from '../core/Domain';

    interface ComponentData {
        title: string;
        items: IListItem[];
    }

    export default Vue.extend({
        components: {
            ListItem
        },
        props: {
            httpClient: { type: Object as PropType<IHttpClient>, required: true },
            list: { type: Object as PropType<IList>, required: true }
        },
        data(): ComponentData {
            return {
                title: '',
                items: []
            }
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

            // this.items = await this.httpClient.httpGet<IListItem[]>('/pages');
        }
    });
</script>
