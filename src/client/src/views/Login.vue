<template>
    <div>
        <form action="/api/users/authenticate" @submit.prevent="doLogin">
            <div class="form-group row">
                <label class="col-sm-2 col-form-label">Username</label>
                <div class="col-sm-10">
                    <input
                        type="text"
                        name="email"
                        v-model="email"
                        class="form-control"
                    />
                </div>
            </div>
            <div class="form-group row">
                <label class="col-sm-2 col-form-label">Password</label>
                <div class="col-sm-10">
                    <input
                        type="password"
                        name="password"
                        v-model="password"
                        class="form-control"
                    />
                </div>
            </div>
            <div class="form-group">
                <button class="form-control">Log In</button>
            </div>
        </form>
    </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';

import { IAuthenticateResponse } from '@/core/Domain';

interface ComponentData {
    email: string;
    password: string;
}

export default defineComponent({
    data(): ComponentData {
        return {
            email: '',
            password: ''
        };
    },
    methods: {
        async doLogin() {
            const data = { email: this.email, password: this.password };
            const response = await this.$api.httpPost<IAuthenticateResponse>(
                '/users/authenticate',
                data
            );
            this.$api.setBearerToken(response.token);
            this.$router.push({ name: 'page' });
        }
    }
});
</script>
