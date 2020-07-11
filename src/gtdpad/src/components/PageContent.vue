<template>
    <div id="page">
        <span>{{ page.title }}</span>
    </div>
</template>

<script lang="ts">
    import Vue, { PropType } from "vue";

    import { IPage } from '../core/IPage';

    interface ComponentData {
        page: IPage;
    }

    export default Vue.extend({
        props: {
            page: Object as PropType<IPage>
        },
        methods: {
        }
    });
</script>
