<template>
    <div class="row">
        <div class="col-8" id="content"><List /></div>
        <nav class="col-4" id="navigation"></nav>
    </div>
</template>

<script lang="ts">
    import Vue from "vue";
    import 'bootstrap';
    import List from './components/List.vue';

    export default Vue.extend({
        components: {
            List
        },
        data: function () {
            return {
                name: 'Hello World, this is GTDPad!',
            }
        }
    });
</script>
