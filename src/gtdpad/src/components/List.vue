<template>
    <ul>
        <ListItem v-for="item in items" :key="item.id" :item="item" />
    </ul>
</template>

<script lang="ts">
    import Vue, { PropType } from "vue";
    import ListItem from './ListItem.vue';

    import { IList } from '../core/Domain';

    interface ComponentData {
        title: string;
        items: any[];
    }

    export default Vue.extend({
        components: {
            ListItem
        },
        props: {
            list: {
                type: Object as PropType<IList>,
                required: true
            }
        },
        data(): ComponentData {
            return {
                title: '',
                items: [
                    { id: '100', title: 'Item 1' },
                    { id: '200', title: 'Item 2' },
                ]
            }
        }
    });
</script>
