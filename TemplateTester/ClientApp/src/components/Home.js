import React from 'react'
import { connect } from 'react-redux'

import { EntityAdder } from './EntityAdder'
import { EntityViewer } from './EntityViewer'

class Home extends React.Component {
	componentDidMount() {
		window.document.title = 'Web UI | Homepage'
	}

	render() {
		return (
			<div>
				<h1>Web UI Homepage</h1>
				<EntityAdder />
				<EntityViewer />
			</div>
		)
	}
}

export default connect(state => state.entities)(Home)
