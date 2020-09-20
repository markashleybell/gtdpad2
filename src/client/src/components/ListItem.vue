<template>
    <li>
        {{ item.text }}
        <input type="text" v-model="updatedText" />
        <button @click="updateTitle">Save</button>
    </li>
</template>

<script lang="ts">
import { defineComponent, PropType } from 'vue';

import { IListItem } from '@/core/Domain';

interface ComponentData {
    text: string;
    updatedText: string;
}

export default defineComponent({
    props: {
        item: { type: Object as PropType<IListItem>, required: true }
    },
    data(): ComponentData {
        return {
            text: this.item.text,
            updatedText: this.item.text
        };
    },
    methods: {
        updateTitle() {
            console.log('PERSIST ITEM TEXT ' + this.item.id);
            this.text = this.updatedText;
            this.$emit('text-updated', this.item.id, this.text);
        }
    }
});
</script>
