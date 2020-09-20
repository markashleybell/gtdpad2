<template>
    <router-view />
</template>

<script lang="ts">
import { defineComponent } from 'vue';

import { IPage } from './core/Domain';

interface ComponentData {
    pages: IPage[];
}

export default defineComponent({
    async created() {
        if (!this.$api.isAuthenticated()) {
            this.$router.push({ name: 'login' });
        } else {
            this.$router.push({ name: 'page' });
        }
    }
});
</script>
