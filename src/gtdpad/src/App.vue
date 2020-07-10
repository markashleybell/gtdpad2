<template>
    <div class="row">
        <div class="col-8" id="content">{{name}}</div>
        <nav class="col-4" id="navigation"></nav>
    </div>
</template>

<script lang="ts">
    import Vue from "vue";
    import 'bootstrap';

    export default Vue.extend({
        data: function () {
            return {
                name: 'Hello World, this is GTDPad!',
            }
        }
    });
</script>
