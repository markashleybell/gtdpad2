<template>
    <ul>
        <li v-for="page in pages" :key="page.id">
            <router-link
                :to="{ name: 'page', params: { id: page.id, slug: page.slug } }"
                >{{ page.title }}</router-link
            >
        </li>
    </ul>
</template>

<script lang="ts">
import { defineComponent, PropType } from 'vue';

import { IPage } from '@/core/Domain';

export default defineComponent({
    props: {
        pages: { type: Array as PropType<IPage[]>, required: true }
    }
});
</script>
