import { createFeatureSelector, createSelector } from '@ngrx/store';
import { AuthState } from '../reducers';

export const getAuthState = createFeatureSelector<AuthState>('auth');

export const getPGPKey = createSelector(getAuthState, (state) => state.userPGPKey);

export const getIsAuthenticated = createSelector(getAuthState, (state) => state.userAuthenticated);

export const getSignUpHasErrors = createSelector(getAuthState, (state) => !!state.signUpError);

export const getSignInHasErrors = createSelector(getAuthState, (state) => !!state.signInError);

export const getSignUpErrors = createSelector(getAuthState, (state) => {
  if (state.signUpError) {
    return state.signUpError.error;
  }
  return '';
});

export const getSignInErrors = createSelector(getAuthState, (state) => {
  if (state.signInError) {
    return state.signInError.error;
  }
  return '';
});
