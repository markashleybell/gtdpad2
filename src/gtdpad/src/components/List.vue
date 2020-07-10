<template>
    <ul>
        <ListItem v-for="item in items" :key="item.id" :item="item" />
    </ul>
</template>

<script lang="ts">
    import Vue from "vue";
    import ListItem from './ListItem.vue'

    export default Vue.extend({
        components: {
            ListItem
        },
        data: function () {
            return {
                items: [
                    { id: '100', title: 'Item 1' },
                    { id: '200', title: 'Item 2' },
                ]
            }
        }
    });
</script>
