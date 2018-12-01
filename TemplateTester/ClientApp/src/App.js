import * as React from'react'
import { Route } from 'react-router'

import Layout from './components/Layout'

import Counter from './pages/Counter/Counter'
import { EntityManager } from './pages/EntityManager/EntityManager'
import FetchData from './pages/DataFetcher/FetchData'
import { Home } from './pages/Home/Home'
import { Idler } from './pages/Idler/Idler'

export default () => (
	<Layout>
		<Route exact path='/' component={Home} />
		<Route path='/counter' component={Counter} />
		<Route path='/fetchdata/:startDateIndex?' component={FetchData} />
		<Route path='/idler' component={Idler} />
		<Route path='/entity-manager' component={EntityManager} />
	</Layout>
)
