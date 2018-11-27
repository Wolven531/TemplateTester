import * as React from'react'
import { Route } from 'react-router'

import Counter from './components/Counter'
import { EntityManager } from './components/EntityManager'
import FetchData from './components/FetchData'
import { Home } from './components/Home'
import { Idler } from './components/Idler'
import Layout from './components/Layout'

export default () => (
	<Layout>
		<Route exact path='/' component={Home} />
		<Route path='/counter' component={Counter} />
		<Route path='/fetchdata/:startDateIndex?' component={FetchData} />
		<Route path='/idler' component={Idler} />
		<Route path='/entity-manager' component={EntityManager} />
	</Layout>
)
