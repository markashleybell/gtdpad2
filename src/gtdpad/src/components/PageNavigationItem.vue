<template>
    <li><a :href="'/pages/' + page.slug" @click="$emit('click', $event, page.id)">{{ page.title }}</a></li>
</template>

<script lang="ts">
    import Vue, { PropType } from 'vue';

    import { IPage } from '../core/Domain';

    export default Vue.extend({
        props: {
            page: { type: Object as PropType<IPage>, required: true }
        }
    });
</script>
