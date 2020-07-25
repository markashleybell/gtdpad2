<template>
    <div>
        <form @submit.prevent="doLogin">
            <div class="form-group row">
                <label class="col-sm-2 col-form-label">Title</label>
                <div class="col-sm-10">
                    <input v-model="email" class="form-control" />
                </div>
            </div>
            <div class="form-group row">
                <label class="col-sm-2 col-form-label">Title</label>
                <div class="col-sm-10">
                    <input v-model="password" class="form-control" />
                </div>
            </div>
            <div class="form-group">
                <button class="form-control">Log In</button>
            </div>
        </form>
    </div>
</template>

<script lang="ts">
    import Vue, { PropType } from 'vue';

    interface ComponentData {
        email: string;
        password: string;
    }

    export default Vue.extend({
        data(): ComponentData {
            return {
                email: '',
                password: ''
            }
        },
        methods: {
            async doLogin() {
                console.log(this.email, this.password);
            }
        }
    });
</script>
