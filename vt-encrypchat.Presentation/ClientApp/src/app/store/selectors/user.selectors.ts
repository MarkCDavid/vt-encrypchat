import {createFeatureSelector, createSelector} from '@ngrx/store';
import {UserState} from '../reducers';


export const getUserState = createFeatureSelector<UserState>('user');

export const getPublicGPGKey = createSelector(getUserState, (state) => state.publicGPGKey);

export const getDisplayName = createSelector(getUserState, (state) => state.displayName);

export const getFoundUsers = createSelector(getUserState, (state) => state.users);
